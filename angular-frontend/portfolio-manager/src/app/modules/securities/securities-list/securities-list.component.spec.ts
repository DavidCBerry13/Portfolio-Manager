import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SecuritiesListComponent } from './securities-list.component';

describe('SecuritiesListComponent', () => {
  let component: SecuritiesListComponent;
  let fixture: ComponentFixture<SecuritiesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SecuritiesListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SecuritiesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
