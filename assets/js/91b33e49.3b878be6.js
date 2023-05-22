"use strict";(self.webpackChunkproper_code=self.webpackChunkproper_code||[]).push([[6052],{3905:(e,t,o)=>{o.d(t,{Zo:()=>p,kt:()=>m});var n=o(7294);function r(e,t,o){return t in e?Object.defineProperty(e,t,{value:o,enumerable:!0,configurable:!0,writable:!0}):e[t]=o,e}function a(e,t){var o=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),o.push.apply(o,n)}return o}function i(e){for(var t=1;t<arguments.length;t++){var o=null!=arguments[t]?arguments[t]:{};t%2?a(Object(o),!0).forEach((function(t){r(e,t,o[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(o)):a(Object(o)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(o,t))}))}return e}function s(e,t){if(null==e)return{};var o,n,r=function(e,t){if(null==e)return{};var o,n,r={},a=Object.keys(e);for(n=0;n<a.length;n++)o=a[n],t.indexOf(o)>=0||(r[o]=e[o]);return r}(e,t);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);for(n=0;n<a.length;n++)o=a[n],t.indexOf(o)>=0||Object.prototype.propertyIsEnumerable.call(e,o)&&(r[o]=e[o])}return r}var l=n.createContext({}),c=function(e){var t=n.useContext(l),o=t;return e&&(o="function"==typeof e?e(t):i(i({},t),e)),o},p=function(e){var t=c(e.components);return n.createElement(l.Provider,{value:t},e.children)},d="mdxType",h={inlineCode:"code",wrapper:function(e){var t=e.children;return n.createElement(n.Fragment,{},t)}},u=n.forwardRef((function(e,t){var o=e.components,r=e.mdxType,a=e.originalType,l=e.parentName,p=s(e,["components","mdxType","originalType","parentName"]),d=c(o),u=r,m=d["".concat(l,".").concat(u)]||d[u]||h[u]||a;return o?n.createElement(m,i(i({ref:t},p),{},{components:o})):n.createElement(m,i({ref:t},p))}));function m(e,t){var o=arguments,r=t&&t.mdxType;if("string"==typeof e||r){var a=o.length,i=new Array(a);i[0]=u;var s={};for(var l in t)hasOwnProperty.call(t,l)&&(s[l]=t[l]);s.originalType=e,s[d]="string"==typeof e?e:r,i[1]=s;for(var c=2;c<a;c++)i[c]=o[c];return n.createElement.apply(null,i)}return n.createElement.apply(null,o)}u.displayName="MDXCreateElement"},4690:(e,t,o)=>{o.r(t),o.d(t,{assets:()=>l,contentTitle:()=>i,default:()=>h,frontMatter:()=>a,metadata:()=>s,toc:()=>c});var n=o(7462),r=(o(7294),o(3905));const a={title:"Day 20: A Regular Map",tags:["Challenges","Advent","C#"]},i=void 0,s={permalink:"/2018/12/20/advent2018-20",source:"@site/blog/2018/12-20-advent2018-20.md",title:"Day 20: A Regular Map",description:"It's back to easier problems with this one.",date:"2018-12-20T00:00:00.000Z",formattedDate:"December 20, 2018",tags:[{label:"Challenges",permalink:"/tags/challenges"},{label:"Advent",permalink:"/tags/advent"},{label:"C#",permalink:"/tags/c"}],readingTime:1.78,hasTruncateMarker:!0,authors:[],frontMatter:{title:"Day 20: A Regular Map",tags:["Challenges","Advent","C#"]},prevItem:{title:"Day 21: Chronal Conversion",permalink:"/2018/12/21/advent2018-21"},nextItem:{title:"Day 19: Go With The Flow",permalink:"/2018/12/19/advent2018-19"}},l={authorsImageUrls:[]},c=[],p={toc:c},d="wrapper";function h(e){let{components:t,...o}=e;return(0,r.kt)(d,(0,n.Z)({},p,o,{components:t,mdxType:"MDXLayout"}),(0,r.kt)("p",null,"It's back to easier problems with this one."),(0,r.kt)("p",null,"My solution was to create a dictionary of rooms and their doors. This is easily done in C# with a flags enum:"),(0,r.kt)("pre",null,(0,r.kt)("code",{parentName:"pre",className:"language-C#"},"[Flags]\nprivate enum RoomDoors\n{\n    None = 0,\n    North = 1,\n    South = 2,\n    East = 4,\n    West = 8\n}\n")),(0,r.kt)("p",null,"This allows to create rooms with multiple doors by combining them as such: ",(0,r.kt)("inlineCode",{parentName:"p"},"RoomDoors.North | RoomDoors.South | RoomDoors.East"),' (this room would have a door on the north side, south side and east side, but no door on the west side). The only "constraint" of flags enum in C# is that your values should be powers of two: think of it as being bits lighting up in a byte. This also allows boolean arithmetic like adding a door to a room: ',(0,r.kt)("inlineCode",{parentName:"p"},"doors |= RoomDoors.North")," and easy checking of doors in a room using ",(0,r.kt)("inlineCode",{parentName:"p"},"doors.HasFlag(RoomDoors.North)"),"."),(0,r.kt)("p",null,"I created a RegexParser class to parse the input using a stack of the current positions. The basic directions pop the stack, add a door depending on the direction and push the new position on the stack. The opening parentheses copy the top position of the stack, the closing parentheses remove the top position and the vertical bar removes the top and copies the second (now top), effectively going back to the previous position and copying it."),(0,r.kt)("p",null,"This class was used to drive a MapBuilder class that contained the doors for each room in a dictionary and a method to add a new door beside a door considering the direction."),(0,r.kt)("p",null,"Once the map was created, I created a DistanceCalculator class that takes the dictionary of rooms with their doors and created dictionary of rooms with their distance from the starting position. To calculate the distance I looped through each room and checked the smallest distance between it's four possible adjacent rooms, increasing it by 1 and saving it to the new room."),(0,r.kt)("p",null,"With this distance map, the first part of the problem is solved by finding the largest distance in the map and the second part is solved by counting the number of distances more than 1000."))}h.isMDXComponent=!0}}]);