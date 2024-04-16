import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CurrencyService } from './currency.service';
import { HttpClient } from '@angular/common/http';

describe('CurrencyService', () => {
    let service: CurrencyService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [CurrencyService, HttpClient]
        });
        service = TestBed.inject(CurrencyService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('should convert currency to text', () => {
        const currencyAmount = 100;
        service.convert({ amount: currencyAmount }).subscribe({
            next: (result: any) => {
                expect(result.result).toBe('One Hundred Dollars');
            }
        });
    });
});