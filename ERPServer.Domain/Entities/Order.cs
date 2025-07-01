using ERPServer.Domain.Abstractions;
using ERPServer.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;
using System;

namespace ERPServer.Domain.Entities;
public sealed class Order : Entity
{
    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public int OrderNumber { get; set; }
    public int OrderNumberYear {  get; set; }
    //private string? _number;

    //public string Number
    //{
    //    get
    //    {
    //        // _number boşsa SetNumber metodunu çağır
    //        if (string.IsNullOrEmpty(_number))
    //        {
    //            _number = SetNumber();
    //        }
    //        return _number;
    //    }
    //    set
    //    {
    //        _number = value; // Burada setter'ı ekliyoruz
    //    }
    //}
    public DateOnly Date { get; set; }
    public DateOnly DeliveryDate { get; set; }

    public OrderStatusEnum Status { get; set; } = OrderStatusEnum.Pending;
    public List<OrderDetail>? Details { get; set; }

    //public string SetNumber()
    //{
    //    string prefix = "BS";
    //    string initialString = prefix + OrderNumberYear.ToString() + Number.ToString();
    //    int targetLengh = 16;
    //    int missingLengh = targetLengh - initialString.Length;
    //    string finalString = prefix + OrderNumberYear.ToString() + new string('0', missingLengh) + OrderNumber.ToString();
    //    return finalString;

    //}
}