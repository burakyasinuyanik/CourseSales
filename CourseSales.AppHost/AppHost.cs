var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CourseSales_Basket_Api>("coursesales-basket-api");

builder.AddProject<Projects.CourseSales_Catalog_Api>("coursesales-catalog-api");

builder.AddProject<Projects.CourseSales_Discount_Api>("coursesales-discount-api");

builder.AddProject<Projects.CourseSales_File_Api>("coursesales-file-api");

builder.AddProject<Projects.CourseSales_Gateway>("coursesales-gateway");

builder.AddProject<Projects.CourseSales_Order_Api>("coursesales-order-api");

builder.AddProject<Projects.CourseSales_Payment_Api>("coursesales-payment-api");

builder.AddProject<Projects.CourseSales_Web>("coursesales-web");

builder.Build().Run();
