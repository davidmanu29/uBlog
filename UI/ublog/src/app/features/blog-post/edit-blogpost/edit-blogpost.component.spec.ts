import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditBlogpostComponent } from './edit-blogpost.component';

describe('EditBlogpostComponent', () => {
  let component: EditBlogpostComponent;
  let fixture: ComponentFixture<EditBlogpostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditBlogpostComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditBlogpostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
