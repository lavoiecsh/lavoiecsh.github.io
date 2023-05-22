"use strict";(self.webpackChunkproper_code=self.webpackChunkproper_code||[]).push([[5389],{3905:(e,t,r)=>{r.d(t,{Zo:()=>c,kt:()=>m});var n=r(7294);function o(e,t,r){return t in e?Object.defineProperty(e,t,{value:r,enumerable:!0,configurable:!0,writable:!0}):e[t]=r,e}function a(e,t){var r=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),r.push.apply(r,n)}return r}function l(e){for(var t=1;t<arguments.length;t++){var r=null!=arguments[t]?arguments[t]:{};t%2?a(Object(r),!0).forEach((function(t){o(e,t,r[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(r)):a(Object(r)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(r,t))}))}return e}function i(e,t){if(null==e)return{};var r,n,o=function(e,t){if(null==e)return{};var r,n,o={},a=Object.keys(e);for(n=0;n<a.length;n++)r=a[n],t.indexOf(r)>=0||(o[r]=e[r]);return o}(e,t);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);for(n=0;n<a.length;n++)r=a[n],t.indexOf(r)>=0||Object.prototype.propertyIsEnumerable.call(e,r)&&(o[r]=e[r])}return o}var s=n.createContext({}),p=function(e){var t=n.useContext(s),r=t;return e&&(r="function"==typeof e?e(t):l(l({},t),e)),r},c=function(e){var t=p(e.components);return n.createElement(s.Provider,{value:t},e.children)},u="mdxType",f={inlineCode:"code",wrapper:function(e){var t=e.children;return n.createElement(n.Fragment,{},t)}},d=n.forwardRef((function(e,t){var r=e.components,o=e.mdxType,a=e.originalType,s=e.parentName,c=i(e,["components","mdxType","originalType","parentName"]),u=p(r),d=o,m=u["".concat(s,".").concat(d)]||u[d]||f[d]||a;return r?n.createElement(m,l(l({ref:t},c),{},{components:r})):n.createElement(m,l({ref:t},c))}));function m(e,t){var r=arguments,o=t&&t.mdxType;if("string"==typeof e||o){var a=r.length,l=new Array(a);l[0]=d;var i={};for(var s in t)hasOwnProperty.call(t,s)&&(i[s]=t[s]);i.originalType=e,i[u]="string"==typeof e?e:o,l[1]=i;for(var p=2;p<a;p++)l[p]=r[p];return n.createElement.apply(null,l)}return n.createElement.apply(null,r)}d.displayName="MDXCreateElement"},4688:(e,t,r)=>{r.r(t),r.d(t,{assets:()=>s,contentTitle:()=>l,default:()=>f,frontMatter:()=>a,metadata:()=>i,toc:()=>p});var n=r(7462),o=(r(7294),r(3905));const a={title:"Day 10: The Stars Align",tags:["Challenges","Advent","C#"]},l=void 0,i={permalink:"/2018/12/10/advent2018-10",source:"@site/blog/2018/12-10-advent2018-10.md",title:"Day 10: The Stars Align",description:"So this one really took me by surprise at the start and had me trying a lot of different stuff to find the best solution.",date:"2018-12-10T00:00:00.000Z",formattedDate:"December 10, 2018",tags:[{label:"Challenges",permalink:"/tags/challenges"},{label:"Advent",permalink:"/tags/advent"},{label:"C#",permalink:"/tags/c"}],readingTime:.795,hasTruncateMarker:!0,authors:[],frontMatter:{title:"Day 10: The Stars Align",tags:["Challenges","Advent","C#"]},prevItem:{title:"Day 11: Chronal Charge",permalink:"/2018/12/11/advent2018-11"},nextItem:{title:"Day 9: Marble Mania",permalink:"/2018/12/09/advent2018-09"}},s={authorsImageUrls:[]},p=[],c={toc:p},u="wrapper";function f(e){let{components:t,...r}=e;return(0,o.kt)(u,(0,n.Z)({},c,r,{components:t,mdxType:"MDXLayout"}),(0,o.kt)("p",null,"So this one really took me by surprise at the start and had me trying a lot of different stuff to find the best solution."),(0,o.kt)("p",null,"I ended up coding a little program to print the contents of the sky to a file and looking it up. At some point I realized the output would probably be the smallest output possible and so I made my solution iterate the moving of the lights until the size of the sky was as small as possible."),(0,o.kt)("p",null,"For the first part of the problem, I just printed out the smallest sky found while iterating and moving the lights. Coding something to recognize the letters would have been a lot more complicated considering I had no idea the shape they used to print them."),(0,o.kt)("p",null,"For the second part of the problem, I used the same method as for the first part and then returned the amount of iterations that were completed."))}f.isMDXComponent=!0}}]);