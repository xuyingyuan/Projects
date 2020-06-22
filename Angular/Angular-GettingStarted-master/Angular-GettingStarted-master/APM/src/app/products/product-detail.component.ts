import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import { IProduct } from './product';
import { ProductService } from './product.service';

@Component({
 
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {

  pageTitle: string = "Product Detail";
  imageWidth:number = 100;
  product: IProduct;
  errorMessage:string = "";

  constructor(private route:ActivatedRoute, private router: Router, private productService:ProductService) { }

  ngOnInit(): void {
    let id=+this.route.snapshot.paramMap.get('id');
    this.pageTitle += ` ${id} `;
    this.getProduct(id);

    
    // {
    //   "productId": 1,
    // "productName": "Leaf Rake",
    // "productCode": "GDN-0011",
    // "releaseDate": "March 19, 2019",
    // "description": "Leaf rake with 48-inch wooden handle.",
    // "price": 19.95,
    // "starRating": 3.2,
    // "imageUrl": "assets/images/leaf_rake.png"
    // };
  }

  getProduct(id:number){
    this.productService.getProduct(id).subscribe({
      next:product=>this.product = product,
      error:err=> this.errorMessage = err
    });
  }

  onBack(): void{
    this.router.navigate(['/products']);
  }

}
