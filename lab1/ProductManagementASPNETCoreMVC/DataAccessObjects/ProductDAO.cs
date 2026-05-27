using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;

namespace DataAccessObjects
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            var listProducts = new List<Product>();
            try
            {
                using var context = new MyStoreContext();
                listProducts = context.Products
                    .Include(p => p.Category)
                    .ToList();
            }
            catch (Exception)
            {
                return listProducts;
            }

            return listProducts;
        }

        public static void SaveProduct(Product p)
        {
            try
            {
                using var context = new MyStoreContext();
                context.Products.Add(p);
                context.SaveChanges(); // Update database
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateProduct(Product p)
        {
            try
            {
                using var context = new MyStoreContext();
                context.Entry<Product>(p).State = EntityState.Modified;
                context.SaveChanges(); // Update database
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteProduct(Product p)
        {
            try
            {
                using var context = new MyStoreContext();
                context.Products.Remove(p);
                context.SaveChanges(); // Update database
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Product GetProductById(int id)
        {
            using var db = new MyStoreContext();
            return db.Products.FirstOrDefault(p => p.ProductId.Equals(id))!;
        }
    }
}

