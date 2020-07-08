using AutoMapper;
using InvoiceManagementApp.Application.Invoices.Commands;
using InvoiceManagementApp.Application.Invoices.ViewModels;
using InvoiceManagementApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceManagementApp.Application.Invoices.MappingProfiles
{
    class InvoiceMappingProfile:Profile
    {
        public InvoiceMappingProfile()
        {
            CreateMap<Invoice, InvoiceVm>();
            CreateMap<InvoiceItem, InvoiceItemVm>(); 
            CreateMap<InvoiceVm, Invoice>();
            CreateMap<InvoiceItemVm, InvoiceItem>();
            CreateMap<CreateInvoiceCommand, Invoice>();

            //here is sample if need manually mapping certain filds
            //CreateMap<InvoiceItem, InvoiceItemVm>().ConstructUsing(i => new InvoiceItemVm
            //{
            //    Id = i.Id,
            //    Item = i.Item,
            //    Quantity = i.Quantity,
            //    Rate = i.Rate
            //});
        }
    }
}
