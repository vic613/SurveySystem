import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormArray, FormBuilder, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';

type GroceryOption = { key: string; label: string };
type OutletOption = { id: number; name: string };

@Component({
  selector: 'app-survey-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './survey-form.component.html',
  template: '<p>Survey List Works!</p>',
  styleUrls: ['./survey-form.component.css']
})
export class SurveyFormComponent {

   // Mock outlet list (later load from API)
  outlets: OutletOption[] = [
    { id: 1, name: 'AEON Mall Kepong' },
    { id: 2, name: 'Lotus’s Selayang' },
    { id: 3, name: 'Giant Batu Caves' },
    { id: 4, name: 'Village Grocer Bangsar' }
  ];

  groceryOptions: GroceryOption[] = [
    { key: 'fresh_produce', label: 'Fresh Produce (Vegetables/Fruits)' },
    { key: 'meat_seafood', label: 'Meat & Seafood' },
    { key: 'dairy_eggs', label: 'Dairy & Eggs' },
    { key: 'bakery', label: 'Bakery' },
    { key: 'frozen', label: 'Frozen Food' },
    { key: 'snacks', label: 'Snacks & Confectionery' },
    { key: 'beverages', label: 'Beverages' },
    { key: 'household', label: 'Household Supplies' },
    { key: 'personal_care', label: 'Personal Care' }
  ];

  form!: ReturnType<FormBuilder['group']>;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      customerName: ['', [Validators.required, Validators.maxLength(200)]],
      age: [null as number | null, [Validators.required, Validators.min(10), Validators.max(120)]],
      location: ['', [Validators.required, Validators.maxLength(200)]],
      outletId: [null, Validators.required],
      groceriesBought: this.fb.array(this.groceryOptions.map(() => new FormControl(false))),
      visitsPerWeek: [null as number | null, [Validators.required, Validators.min(0), Validators.max(21)]],

      notes: ['']
    });
  }

  get groceriesBoughtArray(): FormArray<FormControl<boolean>> {
    return this.form.get('groceriesBought') as FormArray<FormControl<boolean>>;
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    console.log(this.form.value);
  }
}
