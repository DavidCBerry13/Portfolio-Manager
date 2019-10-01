import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { TradeDateService } from './trade-date.service';

describe('TradeDateService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule,
    ],
    providers: [
      TradeDateService,
    ]
  }));

  it('should be created', () => {
    const service: TradeDateService = TestBed.get(TradeDateService);
    expect(service).toBeTruthy();
  });
});
