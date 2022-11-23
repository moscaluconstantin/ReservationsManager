import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoadingSpinnerComponent } from './loading-spinner.component';
import { MaterialModule } from 'src/app/material.module';

@NgModule({
  declarations: [LoadingSpinnerComponent],
  imports: [CommonModule, MaterialModule],
  exports: [LoadingSpinnerComponent],
})
export class LoadingSpinnerModule {}
