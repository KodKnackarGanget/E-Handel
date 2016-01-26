﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using E_Handel.BL;

namespace E_Handel
{
    public partial class Result : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["KKG-EHandelConnectionString"].ConnectionString;

        private List<BLProduct> resultBLProducts = new List<BLProduct>();
        private string searchString;
        private int categoryId;
        private string categoryName;
        private string categoryDescription;

        protected void Page_Load(object sender, EventArgs e)
        {
            searchString = Request.QueryString["search"];
            string categoryIdString = Request.QueryString["categoryId"];
            
            throw new Exception("This is a test exception!");
            if (searchString != null)
            {
                if (ValidateSearchString())
                {
                    LoadSearchResult();
                    DisplaySearchResult();
                }
            }
            else if (categoryIdString != null)
            {
                if (int.TryParse(categoryIdString, out categoryId))
                {
                    LoadCategoryResult();
                    DisplayCategoryResult();
                }
            }
            else if (Request.QueryString["sales"] != null)
            {
                LoadDiscountResult();
                DisplayDiscountResults();
            }
            else
            {
                ResultTitle.InnerHtml = "No search or category results."; //Error getting valid querystring from url
            }
        }

        private void LoadDiscountResult()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlGetDiscounts = new SqlCommand("SELECT ProductID FROM DiscountedProducts", sqlConnection);
            SqlDataReader sqlDiscountDataReader = null;
            try
            {
                sqlConnection.Open();
                sqlDiscountDataReader = sqlGetDiscounts.ExecuteReader();
                while (sqlDiscountDataReader.Read())
                {
                    resultBLProducts.Add(new BLProduct(connectionString, int.Parse(sqlDiscountDataReader["ProductID"].ToString())));
                }
            }
            catch (Exception)
            {
                ResultTitle.InnerHtml = "Error when attempting to retrieve discounted products."; //Error retrieving discounted products from DiscountedProducts or Products
            }
            finally
            {
                if (sqlDiscountDataReader != null)
                {
                    sqlDiscountDataReader.Close();
                    sqlDiscountDataReader.Dispose();
                }
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlGetDiscounts.Dispose();
            }
        }
        private void DisplayDiscountResults()
        {
            ResultTitle.InnerText = "Sales:";
            ResultDescription.InnerText = " Dont miss out of this great sale of among our great products with up to 30 % discount. This sale only applies to the products in our current stock. Dont miss out!! First come, first served!";
            DisplayProducts();
        }

        private bool ValidateSearchString()
        {
            if (searchString.Contains(";"))
                return false;
            return true;
        }
        private void LoadSearchResult()
        {
            string[] searchWords = searchString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string sqlSearchString = "%";
            for (int i = 0; i < searchWords.Length; i++)
                sqlSearchString += searchWords[i] + "%";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlGetSearch = new SqlCommand($"SELECT ID, Name, Description, Price, Popularity, StockQuantity, VAT FROM Products WHERE Name LIKE '{sqlSearchString}'", sqlConnection);
            SqlDataReader sqlProductDataReader = null;
            try
            {
                sqlConnection.Open();

                sqlProductDataReader = sqlGetSearch.ExecuteReader();
                while (sqlProductDataReader.Read())
                {
                    int id = int.Parse(sqlProductDataReader["ID"].ToString());
                    string name = sqlProductDataReader["Name"].ToString();
                    string description = sqlProductDataReader["Description"].ToString();
                    double price = double.Parse(sqlProductDataReader["Price"].ToString());
                    int popularity = int.Parse(sqlProductDataReader["Popularity"].ToString());
                    int stockQuantity = int.Parse(sqlProductDataReader["StockQuantity"].ToString());
                    double VAT = double.Parse(sqlProductDataReader["VAT"].ToString());
                    BLProduct product = new BLProduct(id, categoryId, name, description, price, popularity, stockQuantity, VAT);
                    product.GetDiscountFromDB(connectionString);
                    resultBLProducts.Add(product);
                }
            }
            catch (Exception)
            {
                ResultTitle.InnerHtml = "Error when attempting to search."; //Error when retrieving product from Products
            }
            finally
            {
                if (sqlProductDataReader != null)
                {
                    sqlProductDataReader.Close();
                    sqlProductDataReader.Dispose();
                }
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlGetSearch.Dispose();
            }
        }
        private void DisplaySearchResult()
        {
            ResultTitle.InnerHtml = $"Search results for '{searchString}':";
            DisplayProducts();
        }

        private void LoadCategoryResult()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlGetCategory = new SqlCommand($"SELECT Name, Description FROM Categories WHERE ID = {categoryId}", sqlConnection);
            SqlCommand sqlGetProducts = new SqlCommand($"SELECT ID, Name, Description, Price, Popularity, StockQuantity, VAT FROM Products WHERE CategoryID = {categoryId} AND ID NOT IN (SELECT VariantID FROM ProductVariants)", sqlConnection);
            SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Open();

                sqlDataReader = sqlGetCategory.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    categoryName = sqlDataReader["Name"].ToString();
                    categoryDescription = sqlDataReader["Description"].ToString();
                }
                sqlDataReader.Close();
                sqlDataReader.Dispose();

                sqlDataReader = sqlGetProducts.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    int id = int.Parse(sqlDataReader["ID"].ToString());
                    string name = sqlDataReader["Name"].ToString();
                    string description = sqlDataReader["Description"].ToString();
                    double price = double.Parse(sqlDataReader["Price"].ToString());
                    int popularity = int.Parse(sqlDataReader["Popularity"].ToString());
                    int stockQuantity = int.Parse(sqlDataReader["StockQuantity"].ToString());
                    double VAT = double.Parse(sqlDataReader["VAT"].ToString());
                    BLProduct product = new BLProduct(id, categoryId, name, description, price, popularity, stockQuantity, VAT);
                    product.GetDiscountFromDB(connectionString);
                    resultBLProducts.Add(product);
                }
            }
            catch (Exception)
            {
                ResultTitle.InnerHtml = "Error when attempting to retrieve category products."; //Error when retrieving product from Products
            }
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                    sqlDataReader.Dispose();
                }
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlGetCategory.Dispose();
                sqlGetProducts.Dispose();
            }
        }
        private void DisplayCategoryResult()
        {
            ResultTitle.InnerHtml = categoryName;
            ResultImage.Src = $"ImgHandler.ashx?categoryId={categoryId}";
            ResultDescription.InnerHtml = categoryDescription;
            DisplayProducts();
        }

        private void DisplayProducts()
        {
            int currentColumn = 0;
            const int LAST_COLUMN = 3;
            Panel[] tempPanelArray = new Panel[LAST_COLUMN + 1];

            foreach (BLProduct product in resultBLProducts)
            {
                Panel productPanel = new Panel { CssClass = "span3 result_product_container" };

                HyperLink productLink = new HyperLink {NavigateUrl = $"Product.aspx?productId={product.Id}"};
                Image productThumb = new Image
                {
                    CssClass = "image_thumbnail",
                    ImageUrl = $"ImgHandler.ashx?productIdThumb={product.Id}"
                };
                productLink.Controls.Add(productThumb);
                Label productNameLabel = new Label
                {
                    CssClass = "result_product_name",
                    Text = product.Name
                };
                productPanel.Controls.Add(productLink);
                productPanel.Controls.Add(productNameLabel);

                if (product.Discount > 0)
                {
                    Label productOriginalPriceLabel = new Label
                    {
                        CssClass = "result_product_original_price",
                        Text = "£" + product.Price
                    };
                    productPanel.Controls.Add(productOriginalPriceLabel);
                    double newPrice = product.Price - product.Price * product.Discount / 100;
                    Label productNewPriceLabel = new Label
                    {
                        CssClass = "result_product_discount_price",
                        Text = "£" + newPrice
                    };
                    productPanel.Controls.Add(productNewPriceLabel);
                }
                else
                {
                    Label productPriceLabel = new Label
                    {
                        CssClass = "result_product_price",
                        Text = "£" + product.Price
                    };
                    productPanel.Controls.Add(productPriceLabel);
                }

                Button productInfoButton = new Button
                {
                    CssClass = "result_product_infobutton",
                    Text = "More info",
                    PostBackUrl = $"Product.aspx?productId={product.Id}"
                };
                productPanel.Controls.Add(productInfoButton);

                tempPanelArray[currentColumn] = productPanel;
                currentColumn++;
                if (currentColumn > LAST_COLUMN)
                {
                    Panel productRowPanel = new Panel();
                    Panel productSpanPanel = new Panel();
                    productRowPanel.CssClass = "row-fluid";
                    productSpanPanel.CssClass = "span12";

                    for (int i = 0; i <= LAST_COLUMN; i++)
                        productSpanPanel.Controls.Add(tempPanelArray[i]);

                    productRowPanel.Controls.Add(productSpanPanel);
                    ResultPanel.Controls.Add(productRowPanel);
                    currentColumn = 0;
                }
            }
            if (currentColumn > 0)
            {
                Panel productRowPanel = new Panel();
                Panel productSpanPanel = new Panel();
                productRowPanel.CssClass = "row-fluid";
                productSpanPanel.CssClass = "span12";

                for (int i = 0; i < currentColumn; i++)
                    productSpanPanel.Controls.Add(tempPanelArray[i]);

                productRowPanel.Controls.Add(productSpanPanel);
                ResultPanel.Controls.Add(productRowPanel);
            }
        }
    }
}