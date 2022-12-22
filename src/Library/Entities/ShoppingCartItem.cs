﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities;

public class ShoppingCartItem
{
    public int Id { get; set; }

    public Guid ShoppingCartId { get; set; }

    public Product Product { get; set; } = default!;

    public int ProductId { get; set; }

    public int Quantity { get; set; }
}