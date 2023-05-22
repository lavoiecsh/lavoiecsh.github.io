"use strict";(self.webpackChunkproper_code=self.webpackChunkproper_code||[]).push([[8545],{3905:(e,t,r)=>{r.d(t,{Zo:()=>p,kt:()=>d});var n=r(7294);function o(e,t,r){return t in e?Object.defineProperty(e,t,{value:r,enumerable:!0,configurable:!0,writable:!0}):e[t]=r,e}function a(e,t){var r=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),r.push.apply(r,n)}return r}function i(e){for(var t=1;t<arguments.length;t++){var r=null!=arguments[t]?arguments[t]:{};t%2?a(Object(r),!0).forEach((function(t){o(e,t,r[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(r)):a(Object(r)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(r,t))}))}return e}function l(e,t){if(null==e)return{};var r,n,o=function(e,t){if(null==e)return{};var r,n,o={},a=Object.keys(e);for(n=0;n<a.length;n++)r=a[n],t.indexOf(r)>=0||(o[r]=e[r]);return o}(e,t);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);for(n=0;n<a.length;n++)r=a[n],t.indexOf(r)>=0||Object.prototype.propertyIsEnumerable.call(e,r)&&(o[r]=e[r])}return o}var c=n.createContext({}),s=function(e){var t=n.useContext(c),r=t;return e&&(r="function"==typeof e?e(t):i(i({},t),e)),r},p=function(e){var t=s(e.components);return n.createElement(c.Provider,{value:t},e.children)},u="mdxType",m={inlineCode:"code",wrapper:function(e){var t=e.children;return n.createElement(n.Fragment,{},t)}},f=n.forwardRef((function(e,t){var r=e.components,o=e.mdxType,a=e.originalType,c=e.parentName,p=l(e,["components","mdxType","originalType","parentName"]),u=s(r),f=o,d=u["".concat(c,".").concat(f)]||u[f]||m[f]||a;return r?n.createElement(d,i(i({ref:t},p),{},{components:r})):n.createElement(d,i({ref:t},p))}));function d(e,t){var r=arguments,o=t&&t.mdxType;if("string"==typeof e||o){var a=r.length,i=new Array(a);i[0]=f;var l={};for(var c in t)hasOwnProperty.call(t,c)&&(l[c]=t[c]);l.originalType=e,l[u]="string"==typeof e?e:o,i[1]=l;for(var s=2;s<a;s++)i[s]=r[s];return n.createElement.apply(null,i)}return n.createElement.apply(null,r)}f.displayName="MDXCreateElement"},4804:(e,t,r)=>{r.r(t),r.d(t,{assets:()=>c,contentTitle:()=>i,default:()=>m,frontMatter:()=>a,metadata:()=>l,toc:()=>s});var n=r(7462),o=(r(7294),r(3905));const a={title:"Day 22: Mode Maze",tags:["Challenges","Advent","C#"]},i=void 0,l={permalink:"/2018/12/22/advent2018-22",source:"@site/blog/2018/12-22-advent2018-22.md",title:"Day 22: Mode Maze",description:"The first part for this problem was pretty straight forward, simply calculate the erosion level for each region and transform that into region types before summing the ones in the desired region. Since you're multiplying big integers, at some point there be overflow, but since you're also doing modulo operations, you can work with modulo arithmetic from the start and only conserve the remainder when computing the erosion level of a region.",date:"2018-12-22T00:00:00.000Z",formattedDate:"December 22, 2018",tags:[{label:"Challenges",permalink:"/tags/challenges"},{label:"Advent",permalink:"/tags/advent"},{label:"C#",permalink:"/tags/c"}],readingTime:.83,hasTruncateMarker:!0,authors:[],frontMatter:{title:"Day 22: Mode Maze",tags:["Challenges","Advent","C#"]},prevItem:{title:"Day 24: Immune System Simulator 20XX",permalink:"/2018/12/25/advent2018-24"},nextItem:{title:"Day 21: Chronal Conversion",permalink:"/2018/12/21/advent2018-21"}},c={authorsImageUrls:[]},s=[],p={toc:s},u="wrapper";function m(e){let{components:t,...r}=e;return(0,o.kt)(u,(0,n.Z)({},p,r,{components:t,mdxType:"MDXLayout"}),(0,o.kt)("p",null,"The first part for this problem was pretty straight forward, simply calculate the erosion level for each region and transform that into region types before summing the ones in the desired region. Since you're multiplying big integers, at some point there be overflow, but since you're also doing modulo operations, you can work with modulo arithmetic from the start and only conserve the remainder when computing the erosion level of a region."))}m.isMDXComponent=!0}}]);