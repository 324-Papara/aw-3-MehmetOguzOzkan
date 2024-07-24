using MediatR;
using Para.Base.Response;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Cqrs;

public record CreateCustomerPhoneCommand(CustomerPhoneRequest Request) : IRequest<ApiResponse<CustomerPhoneResponse>>;
public record UpdateCustomerPhoneCommand(long PhoneId, CustomerPhoneRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerPhoneCommand(long PhoneId) : IRequest<ApiResponse>;

public record GetAllCustomerPhonesQuery() : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;
public record GetCustomerPhoneByIdQuery(long PhoneId) : IRequest<ApiResponse<CustomerPhoneResponse>>;
public record GetCustomerPhonesByCustomerIdQuery(long CustomerId) : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;