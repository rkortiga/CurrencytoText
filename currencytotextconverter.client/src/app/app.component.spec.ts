import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { Environment } from '../environment/environment';


describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppComponent],
      imports: [HttpClientTestingModule]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it('should convert currency to text', () => {
    component.conversionForm.get('currencyAmount')?.setValue(100);
    component.convertCurrencyToText();
    const request = httpMock.expectOne(`${Environment.apiUrl}/api/CurrencyToText/ConvertToText`);
    expect(request.request.method).toBe('POST');
    request.flush({ result: 'One Hundred Dollars' });
    expect(component.conversionResult).toBe('One Hundred Dollars');
  });
});