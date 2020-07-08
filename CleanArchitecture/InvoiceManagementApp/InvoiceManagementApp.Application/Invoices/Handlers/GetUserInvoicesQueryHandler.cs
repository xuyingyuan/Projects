using AutoMapper;
using InvoiceManagementApp.Application.Common.Interfaces;
using InvoiceManagementApp.Application.Invoices.Queries;
using InvoiceManagementApp.Application.Invoices.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManagementApp.Application.Invoices.Handlers
{
    class GetUserInvoicesQueryHandler : IRequestHandler<GetUserInvoicesQuery, IList<InvoiceVm>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUserInvoicesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<InvoiceVm>> Handle(GetUserInvoicesQuery request, CancellationToken cancellationToken)
        {
            var vm = new List<InvoiceVm>();
            var invoices = await _context.Invoices.Include(i => i.InvoiceItems)
                .Where(i => i.CreatedBy == request.User).ToListAsync();
            //var vm = invoices.Select(i => new InvoiceVm
            //{
            //    AmountPaid = i.AmountPaid,
            //    Date = i.Date,
            //    Discount = i.Discount,
            //    DiscountType = i.DiscountType,
            //    DueDate = i.DueDate,
            //    From = i.From,
            //    Id = i.Id,
            //    InvoiceNumber = i.InvoiceNumber,
            //    Logo = i.Logo,
            //    PaymentTerms = i.PaymentTerms,
            //    Tax = i.Tax,
            //    TaxType = i.TaxType,
            //    To = i.To,
            //    InvoiceItems = i.InvoiceItems.Select(i => new InvoiceItemVm
            //    {
            //        Id = i.Id,
            //        Item = i.Item,
            //        Quantity = i.Quantity,
            //        Rate = i.Rate
            //    }).ToList(),
            //    Created = i.Created
            //}).ToList();
            if(invoices != null)
            {
                vm = _mapper.Map<List<InvoiceVm>>(invoices);
            }
            return vm;

        }
    }
}
