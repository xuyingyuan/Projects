import{Injectable} from '@angular/core';
import{HttpClient, HttpErrorResponse} from '@angular/common/http';
import{Observable, throwError} from 'rxjs';

import{catchError, tap, map} from 'rxjs/operators';

import { IProduct } from './product';

@Injectable({
    providedIn:"root" //anywhere in application
})
export class ProductService{
    private productUrl='api/products/products.json';

    constructor(private http: HttpClient){}

    getProducts(): Observable<IProduct[]>{
            return this.http.get<IProduct[]>(this.productUrl).pipe(
                tap(data=>console.log('All' + JSON.stringify(data))),
                catchError(this.handleError)
            );
        // return [
        //     {
        //         "productId": 2,
        //         "productName": "Garden Cart",
        //         "productCode": "GDN-0023",
        //         "releaseDate": "March 18, 2019",
        //         "description": "15 gallon capacity rolling garden cart",
        //         "price": 32.99,
        //         "starRating": 4.2,
        //         "imageUrl": "assets/images/garden_cart.png"
        //       },
        //       {
        //         "productId": 5,
        //         "productName": "Hammer",
        //         "productCode": "TBX-0048",
        //         "releaseDate": "May 21, 2019",
        //         "description": "Curved claw steel hammer",
        //         "price": 8.9,
        //         "starRating": 4.8,
        //         "imageUrl": "assets/images/hammer.png"
        //       }
        // ];

    
    }

getProduct(id:number):Observable<IProduct | undefined>{
    return this.getProducts().pipe(
        map((products: IProduct[])=>products.find(p=>p.productId===id))
    ) ;
}

  

    private handleError(err:HttpErrorResponse){
        let errorMessage='';
        if(err.error instanceof ErrorEvent){
            errorMessage = `an error occured ${err.error.message}`;
        }else{
            errorMessage = `server returned code: ${err.status}, error message is ${err.message}`;
        }
        console.error(errorMessage);
        return throwError(errorMessage);
    }
}