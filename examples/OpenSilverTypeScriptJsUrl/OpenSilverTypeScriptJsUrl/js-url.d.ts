// https://github.com/DefinitelyTyped/DefinitelyTyped/blob/17956ab1d255c6e739a2eed81f92e76bc60665b0/types/js-url/index.d.ts
// Type definitions for url v1.8.6
// Project: https://github.com/websanova/js-url
// Definitions by: Pine Mizune <https://github.com/pine>
// Definitions: https://github.com/DefinitelyTyped/DefinitelyTyped

interface UrlStatic {
    (): string;
    (pattern: string): string;
    (pattern: number): string;
    (pattern: string, url: string): string;
    (pattern: number, url: string): string;
}

declare var url: UrlStatic;