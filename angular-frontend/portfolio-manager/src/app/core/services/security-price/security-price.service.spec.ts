import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { SecurityPriceService } from './security-price.service';

describe('SecurityPriceService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule,
    ],
    providers: [
      SecurityPriceService,
    ]
  }));

  it('should be created', () => {
    const service: SecurityPriceService = TestBed.get(SecurityPriceService);
    expect(service).toBeTruthy();
  });
});
