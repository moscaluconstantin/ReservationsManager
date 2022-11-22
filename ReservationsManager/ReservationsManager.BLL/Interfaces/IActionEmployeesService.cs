﻿using ReservationsManager.Common.Dtos.ActionEmployees;
using ReservationsManager.Common.Dtos.Actions;
using ReservationsManager.Common.Dtos.Employees;

namespace ReservationsManager.BLL.Interfaces
{
    public interface IActionEmployeesService
    {
        Task<IEnumerable<AssignedActionDto>> GetActionsAsync();
        Task<IEnumerable<ActionEmployeeDto>> GetAllByEmployeeIDAsync(int employeeID);
        Task<IEnumerable<WorkingEmployeeDto>> GetWorkingEmployeesAsync();
    }
}
