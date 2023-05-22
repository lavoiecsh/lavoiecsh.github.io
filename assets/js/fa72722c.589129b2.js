"use strict";(self.webpackChunkproper_code=self.webpackChunkproper_code||[]).push([[6089],{3905:(e,t,n)=>{n.d(t,{Zo:()=>p,kt:()=>h});var r=n(7294);function a(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function o(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,r)}return n}function l(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?o(Object(n),!0).forEach((function(t){a(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):o(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function i(e,t){if(null==e)return{};var n,r,a=function(e,t){if(null==e)return{};var n,r,a={},o=Object.keys(e);for(r=0;r<o.length;r++)n=o[r],t.indexOf(n)>=0||(a[n]=e[n]);return a}(e,t);if(Object.getOwnPropertySymbols){var o=Object.getOwnPropertySymbols(e);for(r=0;r<o.length;r++)n=o[r],t.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(e,n)&&(a[n]=e[n])}return a}var c=r.createContext({}),s=function(e){var t=r.useContext(c),n=t;return e&&(n="function"==typeof e?e(t):l(l({},t),e)),n},p=function(e){var t=s(e.components);return r.createElement(c.Provider,{value:t},e.children)},u="mdxType",d={inlineCode:"code",wrapper:function(e){var t=e.children;return r.createElement(r.Fragment,{},t)}},m=r.forwardRef((function(e,t){var n=e.components,a=e.mdxType,o=e.originalType,c=e.parentName,p=i(e,["components","mdxType","originalType","parentName"]),u=s(n),m=a,h=u["".concat(c,".").concat(m)]||u[m]||d[m]||o;return n?r.createElement(h,l(l({ref:t},p),{},{components:n})):r.createElement(h,l({ref:t},p))}));function h(e,t){var n=arguments,a=t&&t.mdxType;if("string"==typeof e||a){var o=n.length,l=new Array(o);l[0]=m;var i={};for(var c in t)hasOwnProperty.call(t,c)&&(i[c]=t[c]);i.originalType=e,i[u]="string"==typeof e?e:a,l[1]=i;for(var s=2;s<o;s++)l[s]=n[s];return r.createElement.apply(null,l)}return r.createElement.apply(null,n)}m.displayName="MDXCreateElement"},1893:(e,t,n)=>{n.r(t),n.d(t,{assets:()=>c,contentTitle:()=>l,default:()=>d,frontMatter:()=>o,metadata:()=>i,toc:()=>s});var r=n(7462),a=(n(7294),n(3905));const o={title:"Advent of Code 2019",tags:["Challenges","Advent"]},l=void 0,i={permalink:"/2019/12/02/advent2019",source:"@site/blog/2019/12-02-advent2019.md",title:"Advent of Code 2019",description:"December has come around and it's time for the Advent of Code again!",date:"2019-12-02T00:00:00.000Z",formattedDate:"December 2, 2019",tags:[{label:"Challenges",permalink:"/tags/challenges"},{label:"Advent",permalink:"/tags/advent"}],readingTime:.7,hasTruncateMarker:!0,authors:[],frontMatter:{title:"Advent of Code 2019",tags:["Challenges","Advent"]},prevItem:{title:"Thinking in resolvers",permalink:"/2020/10/15/thinking-in-resolvers"},nextItem:{title:"The problem with java isn't the language, it's the rest",permalink:"/2019/11/26/java"}},c={authorsImageUrls:[]},s=[],p={toc:s},u="wrapper";function d(e){let{components:t,...n}=e;return(0,a.kt)(u,(0,r.Z)({},p,n,{components:t,mdxType:"MDXLayout"}),(0,a.kt)("p",null,"December has come around and it's time for the Advent of Code again!"),(0,a.kt)("p",null,"Last year I completed the challenges using C#, TDD, and way to much architecture. I was also creating a blog post each day to track my progress."),(0,a.kt)("p",null,"This year I challenged myself to use Haskell and not overthink the problems. I won't be creating a blog post each day though, but one every once in a while."),(0,a.kt)("p",null,"As we're December 2nd, I've already completed 2 challenges and here are my thoughts so far on using Haskell:"),(0,a.kt)("ul",null,(0,a.kt)("li",{parentName:"ul"},"Syntax is increadibly easy and readable"),(0,a.kt)("li",{parentName:"ul"},"Thinking in functional gives a good break from day to day OO programming"),(0,a.kt)("li",{parentName:"ul"},"Understanding how to interact with Monads is complicated but is becoming easier each day")),(0,a.kt)("p",null,"As with last year, all my code will be available online: ",(0,a.kt)("a",{parentName:"p",href:"https://github.com/lavoiecsh/lavoiecsh.github.io/tree/master/code/advent2019"},"https://github.com/lavoiecsh/lavoiecsh.github.io/tree/master/code/advent2019")))}d.isMDXComponent=!0}}]);