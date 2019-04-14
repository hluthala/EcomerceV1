using Kissi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kissi.Classes
{
    public class MovementsHelper : IDisposable
    {
        private static KissiContext db = new KissiContext();

        public void Dispose()
        {
            db.Dispose();
        }

        public static Response NewOrder(NewOrderView view, string name)
        {
            using (var transaction=db.Database.BeginTransaction())
            {
                try
                {
                    var user = db.Users.Where(c => c.UserName == name).FirstOrDefault();
                    var order = new Order
                    {
                        CompanyId = user.CompanyId,
                        CustomerId = view.CustomerId,
                        Date = view.Date,
                        Remarks = view.Remarks,
                        StateId = DBHelper.GetState("Created", db),

                    };
                    db.Orders.Add(order);
                    db.SaveChanges();

                    var details = db.OrderDetailTmps.Where(odt => odt.UserName == name).ToList();
                    foreach (var item in details)
                    {
                        var orderdetail = new OrderDetail
                        {
                            Description = item.Description,
                            OrderId = order.OrderId,
                            Price = item.Price,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            TaxRate = item.TaxRate,
                        };
                        db.OrderDetails.Add(orderdetail);
                        db.OrderDetailTmps.Remove(item);

                    }
                    db.SaveChanges();
                    transaction.Commit();
                    return new Response { Succeeded = true, };
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    return new Response {
                        Succeeded = false,
                    Message=ex.Message,

                    };
                }
            }
        }
    }
};