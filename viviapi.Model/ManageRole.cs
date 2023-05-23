using System;

namespace viviapi.Model
{
    [Flags]
    public enum ManageRole
    {
        None = 0,
        News = 1,
        System = 2,
        Interfaces = 4,
        Merchant = 8,
        Orders = 16,
        Financial = 32,
        Report = 128,
    }
}
