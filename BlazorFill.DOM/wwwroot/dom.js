export class DOM {

    /** @param {string} event
     * @param {any} params
     * @returns Promise<void> */
    static async invokeDotNetAsync(event, ...params) {
        // noinspection ES6RedundantAwait
        await DotNet.invokeMethodAsync("BlazorFill.DOM", event, ...params);
    }

    /** @param {({controller: AbortController; hashcode: number})[]} controllers
     * @param {number} hashcode
     * @returns ({controller: AbortController; hashcode: number}) | null */
    static tryAddController(controllers, hashcode) {
        let controller = controllers.find(c => c.hashcode === hashcode);
        if (controller) {
            console.warn(`event with hashcode ${hashcode} has already been registered`);
            return null;
        }
        controller = {controller: new AbortController(), hashcode};
        controllers.push(controller);
        return controller;
    }

    /** @param {({controller: AbortController; hashcode: number})[]} controllers
     * @param {number} hashcode
     * @returns ({controller: AbortController; hashcode: number})[] */
    static tryRemoveEvent(controllers, hashcode) {
        const controller = controllers.find(c => c.hashcode === hashcode);
        if (!controller) {
            console.warn(`event with hashcode ${hashcode} was not found`);
            return controllers;
        }
        controller.controller.abort();
        return controllers.filter(c => c.hashcode !== controller.hashcode);
    }

    /** @param {MouseEvent} args
     * @returns Partial<MouseEvent> */
    static jsToCsharpMouseEvent(args) {
        return {
            detail: args.detail,
            screenX: args.screenX,
            screenY: args.screenY,
            clientX: args.clientX,
            clientY: args.clientY,
            offsetX: args.offsetX,
            offsetY: args.offsetY,
            pageX: args.pageX,
            pageY: args.pageY,
            button: args.button,
            buttons: args.buttons,
            ctrlKey: args.ctrlKey,
            shiftKey: args.shiftKey,
            altKey: args.altKey,
            metaKey: args.metaKey,
            type: args.type
        };
    }

    /** @param {AnimationEvent} args
     * @returns Partial<AnimationEvent> */
    static jsToCsharpAnimationEvent(args) {
        return {
            target: "",
            animationName: args.animationName,
            bubbles: args.bubbles,
            cancelBubble: args.cancelBubble,
            cancelable: args.cancelable,
            composed: args.composed,
            defaultPrevented: args.defaultPrevented,
            elapsedTime: args.elapsedTime,
            eventPhase: args.eventPhase,
            isTrusted: args.isTrusted,
            pseudoElement: args.pseudoElement,
            timeStamp: args.timeStamp,
            type: args.type
        };
    }
}
