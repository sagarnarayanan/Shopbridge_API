using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ShopBridge_CRUD.Models;
using System;
using System.Globalization;

namespace ShopBridge_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductsController(IConfiguration configuration)
        {
            configuration = _configuration;
        }

        string GetConnectionString = "Data Source=127.0.0.1;Initial Catalog=MYDB;User Id=root;";

        [HttpGet]
        public async Task<JsonResult> ListAllProductsAsync()
        {
            string query = @"select A.PRODUCT_NAME,A.PRODUCT_DESCRIPTION,A.PRODUCT_MRP,A.PRODUCT_SELLER from PRODUCTS A";
            DataTable QueryTable = new DataTable();
            //string GetConnectionString = _configuration.GetConnectionString("ShopBridgeAPI");
            MySqlDataReader mdr;
            using (MySqlConnection mycon = new MySqlConnection(GetConnectionString))
            {
                //mycon.Open();
                try
                {
                    await mycon.OpenAsync();

                    using (MySqlCommand mycom = new MySqlCommand(query, mycon))
                    {
                        mdr = mycom.ExecuteReader();
                        QueryTable.Load(mdr);

                        mdr.Close();
                        await mycon.CloseAsync();

                    }
                }
                catch(Exception ex)
                {
                    return new JsonResult(ex.Message + ex.StackTrace);
                }
                
            }
            return new JsonResult(QueryTable);
        }
        [HttpGet("{id}")]
        public async Task<JsonResult> ListSpecificProductsAsync(int ProductID)
        {
            string query = @"select A.PRODUCT_NAME,A.PRODUCT_DESCRIPTION,A.PRODUCT_MRP,A.PRODUCT_SELLER from PRODUCTS A WHERE A.PRODUCT_ID = @PRODUCT_ID";
            DataTable QueryTable = new DataTable();
            //string GetConnectionString = _configuration.GetConnectionString("ShopBridgeAPI");
            MySqlDataReader mdr;
            using (MySqlConnection mycon = new MySqlConnection(GetConnectionString))
            {

                try
                {
                    //mycon.Open();
                    await mycon.OpenAsync();

                    using (MySqlCommand mycom = new MySqlCommand(query, mycon))
                    {
                        mycom.Parameters.AddWithValue("@PRODUCT_ID", ProductID);

                        mdr = mycom.ExecuteReader();
                        QueryTable.Load(mdr);

                        mdr.Close();
                        await mycon.CloseAsync();

                    }
                }
                catch (Exception ex)
                {
                    return new JsonResult(ex.Message + ex.StackTrace);
                }
            }
            return new JsonResult(QueryTable);
        }
        [HttpPost]
        public async Task<JsonResult> InsertProductsAsync(Products products)
        {
            string query = @"insert into MYDB.PRODUCTS(PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESCRIPTION, PRODUCT_MRP, DAT_INSERT_DATE,DAT_MODIFY_DATE, PRODUCT_SELLER) 
                            VALUES(@PRODUCT_ID,@PRODUCT_NAME,@PRODUCT_DESCRIPTION,@PRODUCT_MRP,@PRODUCT_MRP,@DAT_INSERT_DATE,@PRODUCT_SELLER)";

            DataTable QueryTable = new DataTable();
           // string GetConnectionString = _configuration.GetConnectionString("ShopBridgeAPI");
            MySqlDataReader mdr;
            using (MySqlConnection mycon = new MySqlConnection(GetConnectionString))
            {
                try
                {
                    await mycon.OpenAsync();
                    using (MySqlCommand mycom = new MySqlCommand(query, mycon))
                    {
                        mycom.Parameters.AddWithValue("@PRODUCT_ID", products.PRODUCT_ID);
                        mycom.Parameters.AddWithValue("@PRODUCT_NAME", products.PRODUCT_NAME);
                        mycom.Parameters.AddWithValue("@PRODUCT_DESCRIPTION", products.PRODUCT_DESCRIPTION);
                        mycom.Parameters.AddWithValue("@PRODUCT_MRP", products.PRODUCT_MRP);
                        mycom.Parameters.AddWithValue("@DAT_INSERT_DATE", DateTime.Now);
                        mycom.Parameters.AddWithValue("@DAT_MODIFY_DATE", "");
                        mycom.Parameters.AddWithValue("@PRODUCT_SELLER", products.PRODUCT_SELLER);
                        mdr = mycom.ExecuteReader();
                        QueryTable.Load(mdr);

                        mdr.Close();
                        await mycon.CloseAsync();

                    }
                }
                catch (Exception ex)
                {
                    return new JsonResult(ex.Message + ex.StackTrace);
                }
            }
            return new JsonResult("Added Record Successfully");
        }


        [HttpPut]
        public async Task<JsonResult> ModifyProductsAsync(Products products)
        {
            string query = @"UPDATE MYDB.PRODUCTS 
                               SET PRODUCT_NAME = @PRODUCT_NAME,
                                PRODUCT_DESCRIPTION = @PRODUCT_DESCRIPTION, 
                                PRODUCT_MRP = @PRODUCT_MRP, 
                                DAT_MODIFY_DATE = @DAT_MODIFY_DATE,
                                PRODUCT_SELLER = @PRODUCT_SELLER
                                WHERE PRODUCT_ID = @PRODUCT_ID";

            DataTable QueryTable = new DataTable();
            //string GetConnectionString = _configuration.GetConnectionString("ShopBridgeAPI");
            MySqlDataReader mdr;
            using (MySqlConnection mycon = new MySqlConnection(GetConnectionString))
            {
                try
                {
                    await mycon.OpenAsync();
                    using (MySqlCommand mycom = new MySqlCommand(query, mycon))
                    {
                        mycom.Parameters.AddWithValue("@PRODUCT_ID", products.PRODUCT_ID);
                        mycom.Parameters.AddWithValue("@PRODUCT_NAME", products.PRODUCT_NAME);
                        mycom.Parameters.AddWithValue("@PRODUCT_DESCRIPTION", products.PRODUCT_DESCRIPTION);
                        mycom.Parameters.AddWithValue("@PRODUCT_MRP", products.PRODUCT_MRP);
                        mycom.Parameters.AddWithValue("@DAT_MODIFY_DATE", DateTime.Now);
                        mycom.Parameters.AddWithValue("@PRODUCT_SELLER", products.PRODUCT_SELLER);
                        mdr = mycom.ExecuteReader();
                        QueryTable.Load(mdr);

                        mdr.Close();
                        await mycon.CloseAsync();

                    }
                }
                catch (Exception ex)
                {
                    return new JsonResult(ex.Message + ex.StackTrace);
                }
            }
            return new JsonResult("Record Modified Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteProductsAsync(int id)
        {
            string query = @"DELETE FROM MYDB.PRODUCTS 
                                WHERE PRODUCT_ID = @PRODUCT_ID";

            DataTable QueryTable = new DataTable();
           // string GetConnectionString = _configuration.GetConnectionString("ShopBridgeAPI");
            MySqlDataReader mdr;
            using (MySqlConnection mycon = new MySqlConnection(GetConnectionString))
            {
                try
                {
                    await mycon.OpenAsync();
                    using (MySqlCommand mycom = new MySqlCommand(query, mycon))
                    {
                        mycom.Parameters.AddWithValue("@PRODUCT_ID", id);
                        mdr = mycom.ExecuteReader();
                        QueryTable.Load(mdr);

                        mdr.Close();
                        await mycon.CloseAsync();

                    }
                }
                catch (Exception ex)
                {
                    return new JsonResult(ex.Message + ex.StackTrace);
                }

            }
            return new JsonResult("Record Deleted Successfully");
        }


    }
}