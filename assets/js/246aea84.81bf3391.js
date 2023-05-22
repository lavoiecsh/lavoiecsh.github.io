"use strict";(self.webpackChunkproper_code=self.webpackChunkproper_code||[]).push([[9129],{3905:(e,t,o)=>{o.d(t,{Zo:()=>u,kt:()=>m});var r=o(7294);function n(e,t,o){return t in e?Object.defineProperty(e,t,{value:o,enumerable:!0,configurable:!0,writable:!0}):e[t]=o,e}function a(e,t){var o=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),o.push.apply(o,r)}return o}function i(e){for(var t=1;t<arguments.length;t++){var o=null!=arguments[t]?arguments[t]:{};t%2?a(Object(o),!0).forEach((function(t){n(e,t,o[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(o)):a(Object(o)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(o,t))}))}return e}function s(e,t){if(null==e)return{};var o,r,n=function(e,t){if(null==e)return{};var o,r,n={},a=Object.keys(e);for(r=0;r<a.length;r++)o=a[r],t.indexOf(o)>=0||(n[o]=e[o]);return n}(e,t);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);for(r=0;r<a.length;r++)o=a[r],t.indexOf(o)>=0||Object.prototype.propertyIsEnumerable.call(e,o)&&(n[o]=e[o])}return n}var l=r.createContext({}),c=function(e){var t=r.useContext(l),o=t;return e&&(o="function"==typeof e?e(t):i(i({},t),e)),o},u=function(e){var t=c(e.components);return r.createElement(l.Provider,{value:t},e.children)},d="mdxType",p={inlineCode:"code",wrapper:function(e){var t=e.children;return r.createElement(r.Fragment,{},t)}},f=r.forwardRef((function(e,t){var o=e.components,n=e.mdxType,a=e.originalType,l=e.parentName,u=s(e,["components","mdxType","originalType","parentName"]),d=c(o),f=n,m=d["".concat(l,".").concat(f)]||d[f]||p[f]||a;return o?r.createElement(m,i(i({ref:t},u),{},{components:o})):r.createElement(m,i({ref:t},u))}));function m(e,t){var o=arguments,n=t&&t.mdxType;if("string"==typeof e||n){var a=o.length,i=new Array(a);i[0]=f;var s={};for(var l in t)hasOwnProperty.call(t,l)&&(s[l]=t[l]);s.originalType=e,s[d]="string"==typeof e?e:n,i[1]=s;for(var c=2;c<a;c++)i[c]=o[c];return r.createElement.apply(null,i)}return r.createElement.apply(null,o)}f.displayName="MDXCreateElement"},9720:(e,t,o)=>{o.r(t),o.d(t,{assets:()=>l,contentTitle:()=>i,default:()=>p,frontMatter:()=>a,metadata:()=>s,toc:()=>c});var r=o(7462),n=(o(7294),o(3905));const a={title:"Proper Code",tags:["Blog","Update"]},i=void 0,s={permalink:"/2018/10/30/propercode",source:"@site/blog/2018/10-30-propercode.md",title:"Proper Code",description:"I started my programming career in a large waterfall company working on their C++ software. We had multiple layers of analysis documents describing what should be done to implement each feature. We had no unit tests. The software was so large the current version of Visual Studio couldn't handle it all. It took so much time to compile (over six hours to compile everything) that we were doing partial compilations refering to a nightly build and it still took over an hour to build if you had enough code modified. Launching the software to test your features often took over 5 minutes. We were working on two different features at the same time because of all the waiting and you worked on your features alone for weeks if not months at a time. The company was hiring developpers by batches of 20 to 30 and giving them two weeks of classes when they started. Refactoring was pretty much out of the question because we couldn't deliver features fast enough.",date:"2018-10-30T00:00:00.000Z",formattedDate:"October 30, 2018",tags:[{label:"Blog",permalink:"/tags/blog"},{label:"Update",permalink:"/tags/update"}],readingTime:1.79,hasTruncateMarker:!0,authors:[],frontMatter:{title:"Proper Code",tags:["Blog","Update"]},prevItem:{title:"Book Review: Gang of Four",permalink:"/2018/11/06/gangoffour"},nextItem:{title:"Introduction",permalink:"/2018/09/20/indroduction"}},l={authorsImageUrls:[]},c=[],u={toc:c},d="wrapper";function p(e){let{components:t,...o}=e;return(0,n.kt)(d,(0,r.Z)({},u,o,{components:t,mdxType:"MDXLayout"}),(0,n.kt)("p",null,"I started my programming career in a large waterfall company working on their C++ software. We had multiple layers of analysis documents describing what should be done to implement each feature. We had no unit tests. The software was so large the current version of Visual Studio couldn't handle it all. It took so much time to compile (over six hours to compile everything) that we were doing partial compilations refering to a nightly build and it still took over an hour to build if you had enough code modified. Launching the software to test your features often took over 5 minutes. We were working on two different features at the same time because of all the waiting and you worked on your features alone for weeks if not months at a time. The company was hiring developpers by batches of 20 to 30 and giving them two weeks of classes when they started. Refactoring was pretty much out of the question because we couldn't deliver features fast enough."),(0,n.kt)("p",null,"I then switched to a smaller company working in an agile process. I learned about not writing analysis documents, sprint reviews, retros and plannings, unit testing, refactoring, compile and execute cycles requiring seconds instead of minutes. They taught me about Uncle Bob and Clean Code and even offered me to go follow one of his classes."),(0,n.kt)("p",null,"It surprised me how much he was accurate when talking about large companies as it was exactly what I had lived previously. It encouraged me to continue learning even more about clean coding, Test Driven Development (TDD), Domain Driven Design (DDD), architecture and to transfer my knowledge to others around me. It started with doing small sessions with my coworkers, and it is now evolving into this blog."),(0,n.kt)("p",null,"So what is Proper Code? It's everything surrounding clean code, TDD, DDD, architecture. How to code properly so it's easy to make changes down the line and so that the development process doesn't slow down as the code base grows. It's also best practices of the languages and frameworks you work with and knowing the new developments for those languages and frameworks."))}p.isMDXComponent=!0}}]);