import { TestBed } from '@angular/core/testing';

import { ReferenceDataService } from './reference-data.service';

describe('ReferenceDataService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ReferenceDataService = TestBed.get(ReferenceDataService);
    expect(service).toBeTruthy();
  });
});
