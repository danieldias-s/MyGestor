import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RelatorioDoMesComponent } from './relatorio.component';

describe('RelatorioDoMesComponent', () => {
  let component: RelatorioDoMesComponent;
  let fixture: ComponentFixture<RelatorioDoMesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RelatorioDoMesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RelatorioDoMesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
