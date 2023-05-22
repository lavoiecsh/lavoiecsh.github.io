"use strict";(self.webpackChunkproper_code=self.webpackChunkproper_code||[]).push([[9779],{3905:(e,t,r)=>{r.d(t,{Zo:()=>p,kt:()=>h});var n=r(7294);function o(e,t,r){return t in e?Object.defineProperty(e,t,{value:r,enumerable:!0,configurable:!0,writable:!0}):e[t]=r,e}function a(e,t){var r=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),r.push.apply(r,n)}return r}function l(e){for(var t=1;t<arguments.length;t++){var r=null!=arguments[t]?arguments[t]:{};t%2?a(Object(r),!0).forEach((function(t){o(e,t,r[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(r)):a(Object(r)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(r,t))}))}return e}function i(e,t){if(null==e)return{};var r,n,o=function(e,t){if(null==e)return{};var r,n,o={},a=Object.keys(e);for(n=0;n<a.length;n++)r=a[n],t.indexOf(r)>=0||(o[r]=e[r]);return o}(e,t);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);for(n=0;n<a.length;n++)r=a[n],t.indexOf(r)>=0||Object.prototype.propertyIsEnumerable.call(e,r)&&(o[r]=e[r])}return o}var s=n.createContext({}),c=function(e){var t=n.useContext(s),r=t;return e&&(r="function"==typeof e?e(t):l(l({},t),e)),r},p=function(e){var t=c(e.components);return n.createElement(s.Provider,{value:t},e.children)},f="mdxType",u={inlineCode:"code",wrapper:function(e){var t=e.children;return n.createElement(n.Fragment,{},t)}},m=n.forwardRef((function(e,t){var r=e.components,o=e.mdxType,a=e.originalType,s=e.parentName,p=i(e,["components","mdxType","originalType","parentName"]),f=c(r),m=o,h=f["".concat(s,".").concat(m)]||f[m]||u[m]||a;return r?n.createElement(h,l(l({ref:t},p),{},{components:r})):n.createElement(h,l({ref:t},p))}));function h(e,t){var r=arguments,o=t&&t.mdxType;if("string"==typeof e||o){var a=r.length,l=new Array(a);l[0]=m;var i={};for(var s in t)hasOwnProperty.call(t,s)&&(i[s]=t[s]);i.originalType=e,i[f]="string"==typeof e?e:o,l[1]=i;for(var c=2;c<a;c++)l[c]=r[c];return n.createElement.apply(null,l)}return n.createElement.apply(null,r)}m.displayName="MDXCreateElement"},7530:(e,t,r)=>{r.r(t),r.d(t,{assets:()=>s,contentTitle:()=>l,default:()=>u,frontMatter:()=>a,metadata:()=>i,toc:()=>c});var n=r(7462),o=(r(7294),r(3905));const a={title:"Day 17: Reservoir Research",tags:["Challenges","Advent","C#"]},l=void 0,i={permalink:"/2018/12/17/advent2018-17",source:"@site/blog/2018/12-17-advent2018-17.md",title:"Day 17: Reservoir Research",description:"This one took me a while because I was a little off on my first answers. I forgot to remove the first empty lines of the clay map, so I was 9 water tiles over the correct answer.",date:"2018-12-17T00:00:00.000Z",formattedDate:"December 17, 2018",tags:[{label:"Challenges",permalink:"/tags/challenges"},{label:"Advent",permalink:"/tags/advent"},{label:"C#",permalink:"/tags/c"}],readingTime:.6,hasTruncateMarker:!0,authors:[],frontMatter:{title:"Day 17: Reservoir Research",tags:["Challenges","Advent","C#"]},prevItem:{title:"Day 16: Chronal Classification",permalink:"/2018/12/17/advent2018-16"},nextItem:{title:"Day 14: Chocolate Charts",permalink:"/2018/12/14/advent2018-14"}},s={authorsImageUrls:[]},c=[],p={toc:c},f="wrapper";function u(e){let{components:t,...r}=e;return(0,o.kt)(f,(0,n.Z)({},p,r,{components:t,mdxType:"MDXLayout"}),(0,o.kt)("p",null,"This one took me a while because I was a little off on my first answers. I forgot to remove the first empty lines of the clay map, so I was 9 water tiles over the correct answer."),(0,o.kt)("p",null,"Apart from that, the first part of the problem took me a while to come up with a decent solution. I ended up creating a small algorithm to calculate flows which fills a reservoir and reinserts new flows into a queue for the main algorithm to go through."),(0,o.kt)("p",null,"The second part of the problem was probably one of the easiest modifications I had to do to get the answer. I counted only 1 type of tile instead of 2."))}u.isMDXComponent=!0}}]);