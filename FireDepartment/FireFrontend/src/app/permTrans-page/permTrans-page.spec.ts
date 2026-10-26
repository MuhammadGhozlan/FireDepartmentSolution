import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PermTransPage } from './permTrans-page';

describe('PermTransPage', () => {
  let component: PermTransPage;
  let fixture: ComponentFixture<PermTransPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PermTransPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PermTransPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
