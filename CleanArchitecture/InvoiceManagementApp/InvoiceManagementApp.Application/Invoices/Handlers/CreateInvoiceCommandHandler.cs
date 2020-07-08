using AutoMapper;
using InvoiceManagementApp.Application.Common.Interfaces;
using InvoiceManagementApp.Application.Invoices.Commands;
using InvoiceManagementApp.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManagementApp.Application.Invoices.Handlers
{
    class CreateInvoiceCommandHandler:IRequestHandler<CreateInvoiceCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateInvoiceCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            //var entity = new Invoice
            //{
            //    AmountPaid = request.AmountPaid,
            //    Date = request.Date,
            //    DueDate = request.DueDate,
            //    Discount = request.Discount,
            //    DiscountType = request.DiscountType,
            //    From = request.From,
            //    InvoiceNumber = request.InvoiceNumber,
            //    Logo = request.Logo,
            //    PaymentTerms = request.PaymentTerms,
            //    Tax = request.Tax,
            //    TaxType = request.TaxType,
            //    To = request.To,
            //    InvoiceItems = request.InvoiceItems.Select(i => new InvoiceItem
            //    {
            //        Item = i.Item,
            //        Quantity = i.Quantity,
            //        Rate = i.Rate
            //    }).ToList()
            //};

            var entity = _mapper.Map<Invoice>(request);
            _context.Invoices.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}

