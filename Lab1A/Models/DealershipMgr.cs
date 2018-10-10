using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1A.Models
{
    public class DealershipMgr
    {
        public DealershipMgr()
        {

        }
         
        public DbSet<Lab1A.Models.Dealership> Dealership { get; set; }

        List<Dealership> dealershipList = new List<Dealership> { new Dealership {ID = 1, Name="Honda Dealership", Email="Honda.Dealership@gmail.com", PhoneNumber="1234567899" },
                                                                        new Dealership{ID = 2, Name="Nissan Dealership", Email="Nissan.Dealership@gmail.com", PhoneNumber="9876543211" },
                                                                        new Dealership{ID = 3, Name="BMW Dealership", Email="BMW.Dealership@gmail.com", PhoneNumber="9055155678" }
                                                                      };

        public void SeedData(DealershipMgr myDealership)
        {
            myDealership.Dealership.AddRange(dealershipList);
        }


        public List<Dealership> DealershipList()
        {
            return dealershipList;

        }


    }
}
