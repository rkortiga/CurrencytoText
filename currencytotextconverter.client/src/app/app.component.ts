import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { CurrencyService } from './service/currency.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  ngUnsubscribe: Subject<void> = new Subject<void>();
  public conversionResult: string = '';
  public conversionForm: FormGroup;

  constructor(
    private currencyService: CurrencyService,
    private formBuilder: FormBuilder
  ) {
    this.conversionForm = this.formBuilder.group({
      currencyAmount: ['', Validators.required]
    });
  }

  ngOnInit() {}

  convertCurrencyToText() {
    if (this.conversionForm.valid) {
      const currencyAmount = this.conversionForm.get('currencyAmount')?.value;
      this.currencyService.convert({ amount: currencyAmount }).pipe(takeUntil(this.ngUnsubscribe)).subscribe({
        next: (result: any) => {
          this.conversionResult = result.result;
          console.log(result);
        },
        error: (error) => {
          alert('An error occurred while converting currency to text');
        }
      });
    }
    else if (this.conversionForm.get('currencyAmount')?.hasError('required')) {
      alert('Please enter a valid currency amount');
    }
  }
}
