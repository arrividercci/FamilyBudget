using FamilyBudget.WebServer.Data.Entities;
using FamilyBudget.WebServer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget.WebServer.Data.Extensions
{
    public static class PurchaseExtensions
    {
        public static PurchaseDto? ToModel(this Purchase? purchase)
        {
            if (purchase == null)
            {
                return null;
            }
            else
            {
                return new PurchaseDto()
                {
                    Date = purchase.Date,
                    Name = purchase.Name,
                    Price = purchase.Price,
                };
            }
        }

        public static PurchaseForFamilyDto? ToFamilyModel(this Purchase? purchase)
        {
            if (purchase == null)
            {
                return null;
            }
            else
            {
                return new PurchaseForFamilyDto()
                {
                    Date = purchase.Date,
                    Name = purchase.Name,
                    Price = purchase.Price,
                    FamilyMemberName = purchase.FamilyMemberName
                };
            }
        }

        public static Purchase ToEntity(this PurchaseDto purchase)
        {
            return new Purchase()
            {
                Date = purchase.Date,
                Name = purchase.Name,
                Price = purchase.Price,
            };
        }

        public static Purchase ToFamilyEntity(this PurchaseDto purchase, string userName)
        {
            return new Purchase()
            {
                Date = purchase.Date,
                Name = purchase.Name,
                Price = purchase.Price,
                FamilyMemberName = userName
            };
        }
    }
}
