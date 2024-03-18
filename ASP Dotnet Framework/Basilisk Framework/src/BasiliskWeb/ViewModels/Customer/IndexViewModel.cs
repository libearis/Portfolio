﻿namespace BasiliskWeb.ViewModels.Customer;

public class IndexViewModel
{
    public List<CustomerViewModel> Customers { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string CompanyName { get; set; }
    public string ContactPerson { get; set; }
}
