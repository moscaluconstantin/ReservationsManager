import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LoadingService {
  get isLoading(): boolean {
    return this.loading;
  }

  private loading: boolean = false;

  constructor() {}

  startLoading(): void {
    this.loading = true;
  }

  finishLoading(): void {
    this.loading = false;
  }
}
