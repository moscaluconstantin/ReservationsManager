import { Observable, of } from 'rxjs';

export function handleError<T>(operation = 'operation', result?: T) {
  return (error: any): Observable<T> => {
    console.error(error);
    console.error(`${operation} failed: ${error.message}`);
    return of(result as T);
  };
}
