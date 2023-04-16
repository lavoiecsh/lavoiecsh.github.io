---
title: "React + Redux + Saga"
tags: [Practices,Javascript]
---
After working a little with the React-Redux-Saga ecosystem, and working with others that were still new to it, I came to realize some parts of the frameworks can be a little confusing so I decided to write this post to explain how everything works together. This article isn't meant to explain everything, but give a brief overview of the different sections of the ecosystem and how they interact with each other in case you need to work on an existing project or need to think about which technology to use for new project.

<!-- truncate -->

## React
React is essentially a framework to easily divide your front-end application in different components which interact together to form your application. This helps separate concerns for the different parts of your application and reuse some of the components you create.

You define your components using either a class or a function syntax:
```javascript
class Welcome extends React.Component {
    render() {
        return <h1>Hello, {this.props.name}</h1>;
    }
}

function Welcome(props) {
    return <h1>Hello, {props.name}</h1>;
}

function Welcome(props) => <h1>Hello, {props.name}</h1>;
```
All these define a `Welcome` component that can be used inside other components with `<Welcome name="myname"/>`. In the later version of React, function components can do anything a class component can, the only difference being the syntax.

### Lifecycle
A big part of React's component is the lifecycle. It's important to understand what is available at which point in the lifecycle. New components follow four basic steps: 

1. constructor (for class components)
2. `render` function for class components or the function itself for function components
3. React updates the DOM
4. `componentDidMount` (for class components)

This means that React renders everything inside a temporary buffer before sending it to the DOM for the user to see and interact with. For most components, the `render` function is enough to handle everything needed. In cases where you want to do something when the page loads or is loaded, your best bet is the `componentDidMount` function. This ensures the component is properly mounted before you change anything inside it, as changes before mounting is done will not be visible.

Updating a component is triggered either by changing their properties, using the `setState` function inside the component or calling `forceUpdate` for your component. In this case, the component will follow these three steps:

1. `render`
2. React updates the DOM
3. `componentDidUpdate`

When unmounting a component, it will only call the `componentWillUnmount` if it is defined.

### Properties
Every component defines a set of properties that are used to customize it. Properties cannot be modified inside the component, they are simply there to change the behaviour of the component when it renders, or handles any action. You can define the properties you're expecting in your component as well as types for these properties by adding a propTypes property on your component class:
```javascript
import PropTypes from 'prop-types';

Welcome.propTypes = {
    name: PropTypes.string
};
```
The advantage of this is that most editors read these and help you with autocompletion and errors if you didn't add a required property or added an undefined property. You can also define defaults for the properties:
```javascript
Welcome.defaultProps = {
    name: 'stranger'
};
```

### Unit testing
Unit testing your components can be done with `enzyme`, a renderer for React components which let's you inspect the components to validate that they will be displayed correctly. Enzyme offers three rendering methods, all of which define a function taking your component as argument and returning a Renderer object containing methods to inspect the output:
#### Shallow Rendering
`shallow(node) => ShallowRenderer`
This renders the component without rendering any child component. This means that the output of shallow will contain the tags specified by the used components and not their html representation. This is particularly useful when you want to unit test a component, and whether it correctly adds a child component or not.
#### Full DOM Rendering
`mount(node) => ReactWrapper`
This renders the components and all its children. The output will contain valid html equivalent to what is being rendered to the user when using the application. This can be useful to test interactions with the DOM APIs. It requires a DOM, which is available in a browser or with the jsdom library. Since it is mounting the HTML in the DOM (similarly to how React actually works), you may need to use unmount between tests to clean your DOM so your tests don't impact each other.
#### Static Rendering
`render(node) => CheerioWrapper`
This uses Cheerio, a library to render static HTML, to render the HTML statically for your component and its children. It only does rendering, it does not call React's component lifecycle and does not interact with the DOM. This is rarely used in practice since `shallow` and `mount` offer more without increasing testing time. 

## Redux
Redux is a library on top of React which adds a sharable state between the components as well as an event-driven architecture to help the components interact with each other.
### Actions
Redux lets you define actions which you can trigger manually whenever something happens. This is the publishing part of the event-driven architecture. Actions can also contain additional data. They are defined with the `createAction` function and triggered by calling the returned function:
```javascript
// define the action in a shared file
export buttonPressed = createAction('BUTTON_PRESSED');
// use the action in your component
buttonPressed(additionalData);
```
### Reducers
Reducers are functions triggered whenever a specific action is triggered. Their purpose is to update the global state depending on the triggered action and its additional data. They are created with the `handleAction` and `handleActions` functions with an initial state and a function mapping the received state to the new global state:
```javascript
handleActions({
    BUTTON_PRESSED: (state) => ({ ...state, somethingElse: true }),
    OTHER_BUTTON_PRESSED: (state) => ({ somethingElse: false })
}, { somethingElse: false });
```

### Connected Components
Since React updates the components only when their properties are changed, they will not be updated when the global state changes unless they are connected to the Redux engine. To connect your component, you need to define a `mapStateToProps` function that reads the new state and maps values from it to your components properties. You can also define a `mapDispatchToProps` object that will set some of your properties to actions you created previously (similarly to the `defaultProps` object). With all this, you then export a connected component instead of your previous component with the `connect` function:
```javascript
import buttonPressed from './actions';

mapStateToProps = (state) => ({
    myProp: state.somethingElse
});

mapDispatchToProps = {
    buttonPressed
};

export default connect(mapStateToProps, mapDispatchToProps)(Welcome);
```
### Event-driven architecture
As mentioned, Redux uses an event-driven architecture style which will usually follow this lifecycle:
1. User interacts with your component
2. Component triggers an action
3. All reducers for that action will be called to modify the global state
4. mapStateToProps will be called on each component with the new global state
5. any component that changes a property during mapStateToProps will trigger it's update lifecycle and be updated

This helps to separate concerns inside your application. Actions are just placeholders to separate the different actions that can happen in your application. Reducers only need to know how to modify the state whenever a specific action is called. Components only need to know which action to trigger and their `mapStateToProps` only need to know what to do with the new state.

It also allows you to make modifications to other components from a component since all the reducers and `mapStateToProps` will be executed when an action is triggered.

The biggest thing to remember when using actions, reducers and connect components is that you want each action to be as simple as possible. This is why thinking in events is usually easier: text changed, button clicked, etc. Since the actions, reducers and state are global to your application, make sure to name them well and not reuse the same names for actions or state values.

### Unit testing
Actions cannot be unit tested as they are just definitions.

Reducers can easily be unit tested by ensuring the new state reflects the expected changes depending on the triggered action and additional information. The `handleActions` function returns a `reducer` function that takes an initial state and a object containing the type of triggered action (the string argument when you `createAction`) and the additional data as a payload. Executing this function returns the new state, making unit testing very simple:
```javascript
expect(reducer({
    // empty initial state
}, {
    type: BUTTON_PRESSED,
    payload: { newValue: 'test' }
})).toEqual({
    newValue: 'test',
    somethingElse: true
});
```

For components, you will need to unit test the initial rendering (as before), the `mapStateToProps` function which is pretty straight forward and make sure that actions are correctly triggered when an action happens. This requires shallow or full DOM rendering (static rendering doesn't work).
```javascript
define('welcome', () => {
    let component;
    const props = {
        buttonPressed: jest.fn(),
        name: 'test'
    };
    
    beforeEach(() => {
        jest.clearAllMocks();
        component = shallow(<Welcome {...props}/>);    
    });

    it('renders correctly', () => {
        expect(component.find('something').props().name).toEqual(props.name);    
    });

    it('triggers action when button is pressed', () => {
        component.find('button').simulate('click');

        expect(props.buttonPressed).toHaveBeenCalledWith({ name: 'test' });
    });
});
```

## Saga
As you probably guessed, some things are still a little complicated when using React with Redux, especially handling promises and such when calling apis to fetch data or make modifications. The React-Redux-Saga library helps greatly with this. Saga is a design pattern initially defined to handle interactions with multiple components you may or may not control. It is especially useful when your application must call multiple apis and revert each call if one of them failed.

Most of the time when you need to fetch data with React-Redux, you'll end up with a call to an api that returns a promise. Once it resolves, you trigger the action associated with it. For simple cases like this, you may find Saga a little overwhelming, but for more complicated cases it greatly helps to reduce duplication and helps extract api logic from your components.

In React-Redux-Saga, you define generator functions that are executed when an action is triggered. They essentially act as a reducer with more logic.
```javascript
function* mySaga() {
    // execute when buttonPressed is triggered
    const additionalData = yield take('BUTTON_PRESSED');
    // fetch some data
    const initialData = yield call(fetch(additionalData.id));
    // fetch some other data
    const moreData = yield call(fetchOther(initialData.something));
    // fetch some state value
    const stateData = yield select(state => state.somethingElse);
    // trigger an action with the fetched data
    yield put(dataFetched({ ...initialData, ...moreData, ...stateData }));
}
```
### Unit testing
There are two methods to unit testing sagas. The first method goes through the generator function and expects the returns at each yield. This tightly couples your test code to your saga code, making any changes painful since you'll need to modify all the tests whenever you need to change the saga code.
```javascript
const gen = mySaga();
expect(gen.next()).toEqual(take('BUTTON_PRESSED'));
expect(gen.next({ id: 'id' })).toEqual(fetch('id'));
expect(gen.next({ something: 2 })).toEqual(fetchOther(2));
expect(gen.next({ blah: [] })).toEqual(select(state => state.somethingElse));
expect(gen.next({ test: 'abc' })).toEqual(put(dataFetched({ id: 'id', something: 2, blah: [], test: 'abc'})));
expect(gen.next()).toEqual({ done: true, value: undefined });
```
This can quickly become painful to maintain and debug if there is a problem.

The other method is to actually run the saga and expect the resulting dispatched actions.
```javascript
fetch = jest.fn().mockReturnValue({ something: 2 });
fetchOther = jest.fn().mockReturnValue({ blah: [] });

const dispatched = [];
const saga = await runSaga({
    dispatch: (action) => dispatched.push(action),
    getState: () => ({ somethingElse: 'abc' })
}, mySaga, {id:'id'}).toPromise();

expect(dispatched).toEqual([
    dataFetched({ id: 'id', something: 2, blah: [], test: 'abc' })
]);
```
Since you only expect the dispatch items to contain the desired actions and data, changing the saga code has less chances to break the tests unless the actual logic of the saga changes. With more complicated sagas, it becomes a much more powerful tool since you're actually testing the output of the function depending on the input and not the implementation.
