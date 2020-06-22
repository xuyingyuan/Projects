import { Component, OnInit } from '@angular/core';
import { IProduct } from './product';
import { ProductService } from './product.service';

@Component({
   // selector: 'pm-products',
    templateUrl:'./product-list.component.html',
    styleUrls:['./product-list.component.css']
})
export class ProductListComponent implements OnInit{
    pageTitle: string='My Product List';
    imageWidth: number=50;
    imageMargin: number=2;
    showImage: boolean=false;
    errorMessage:string;

   _listFilter: string;
   get listFilter(): string{
     return this._listFilter;
   }
   set listFilter(value:string){
     this._listFilter = value;
     this.filteredProducts=this.listFilter? this.performFilter(this.listFilter) : this.products;
   }

    filteredProducts: IProduct[];
    products: IProduct[];
  


   constructor(private productService: ProductService){       
   }
 
   onRatingClicked(message:string):void{
     this.pageTitle = 'product list ' + message;
   }

   performFilter(filterBy:string) : IProduct[]{
     filterBy = filterBy.toLowerCase();
     return this.products.filter((product:IProduct)=>product.productName.toLowerCase().indexOf(filterBy) !== -1);
   }
   toggleImage(): void{
     this.showImage=!this.showImage;
   }

   ngOnInit(): void {
    // this.products = this.productService.getProducts();
    this.productService.getProducts().subscribe({
      next:products=>{this.products= products;
        this.filteredProducts = this.products;
      },
      error:err=>this.errorMessage=err});
    // this.productService.getProducts().subscribe({
    //   next(products){console.log(products)},
    //   error(err){console.log(err)}
    // })
    //this.filteredProducts = this.products;
    
}
}
// before  use "ng g c products/product-detail --flat"
// i have to use these commoand to check currentuser: 
//Get-ExecutionPolicy -list
//then I have to run this command: Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
