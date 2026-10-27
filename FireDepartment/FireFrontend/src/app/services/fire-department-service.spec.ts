import { TestBed } from '@angular/core/testing';

import { FireDepartmentService } from './fire-department-service';

describe('FireDepartmentService', () => {
  let service: FireDepartmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FireDepartmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
