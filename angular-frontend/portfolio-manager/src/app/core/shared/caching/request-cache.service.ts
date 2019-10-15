import { Injectable } from '@angular/core';
import { HttpRequest, HttpResponse } from '@angular/common/http';


export interface RequestCacheEntry {
  url: string;
  response: HttpResponse<any>;
  cacheTime: number;
}

export abstract class RequestCache {
  abstract get(req: HttpRequest<any>): HttpResponse<any> | undefined;
  abstract put(req: HttpRequest<any>, response: HttpResponse<any>): void;
}

const maxAge = 30000; // maximum cache age (ms)

@Injectable()
export class RequestCacheWithMap implements RequestCache {

  cache = new Map<string, RequestCacheEntry>();

  constructor() { }

  get(req: HttpRequest<any>): HttpResponse<any> | undefined {
    const url = req.urlWithParams;
    const cached = this.cache.get(url);

    if (!cached) {
      return undefined;
    }

    const isExpired = cached.cacheTime < (Date.now() - maxAge);
    if (isExpired) {
        console.log(`Found cached value for "${url}" but it is expired.`);
        this.cache.delete(url);
        return undefined;
    } else {
        return cached.response;
    }

  }

  put(req: HttpRequest<any>, response: HttpResponse<any>): void {
    const url = req.urlWithParams;
    console.log(`Caching response from "${url}".`);

    const entry = { url, response, cacheTime: Date.now() };
    this.cache.set(url, entry);

  }

  pruneCache(): void {
    const expired = Date.now() - maxAge;
    this.cache.forEach(entry => {
      if (entry.cacheTime < expired) {
        this.cache.delete(entry.url);
      }
    });
  }

}
