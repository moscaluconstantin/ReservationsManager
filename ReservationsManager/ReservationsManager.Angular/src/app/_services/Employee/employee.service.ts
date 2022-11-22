import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WorkingEmployeeDto } from 'src/app/_models/Employee/WorkingEmployeeDto';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  baseUrl = environment.apiUrl + 'Employees/';

  constructor(private http: HttpClient) {}

  getEmployeesAssignedToAction(
    actionId: number
  ): Observable<Array<WorkingEmployeeDto>> {
    return this.http.get<Array<WorkingEmployeeDto>>(
      `${this.baseUrl}AssignedToAction/${actionId}`
    );
  }
}
