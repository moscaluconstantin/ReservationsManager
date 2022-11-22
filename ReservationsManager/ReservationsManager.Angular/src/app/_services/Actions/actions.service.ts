import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AssignedActionDto } from 'src/app/_models/Action/AssignedActionDto';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ActionsService {
  baseUrl = environment.apiUrl + 'ActionEmployees/';

  constructor(private http: HttpClient) {}

  getAssignedActions(): Observable<Array<AssignedActionDto>> {
    return this.http.get<Array<AssignedActionDto>>(
      `${this.baseUrl}AssignedActions`
    );
  }
}
