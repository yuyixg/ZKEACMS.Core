/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using Easy.RepositoryPattern;
using ZKEACMS.Shop.Models;

namespace ZKEACMS.Shop.Service
{
    public interface IPaymentService
    {
        string Getway { get; }
        PaymentInfo GetPaymentInfo(Order order);
        ServiceResult<bool> Refund(Order order);
        RefundInfo GetRefund(Order order);
        ServiceResult<bool> CloseOrder(Order order);
    }
}
