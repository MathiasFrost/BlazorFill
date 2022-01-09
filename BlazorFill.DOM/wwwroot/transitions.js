import {DOM} from "./dom.js";

/** @type ({controller: AbortController; hashcode: number})[] */
let controllers = [];

// noinspection JSUnusedGlobalSymbols
/** @param {string} event
 * @param {number} hashcode
 * @param {HTMLElement} el
 * @param {({start?: boolean; end?: boolean; cancel?: boolean; run?: boolean})} options
 * @returns void */
export function addTransitionEvents(event, hashcode, el, options) {
    const controller = DOM.tryAddController(controllers, hashcode);
    if (!controller) return;
    addEvents(event, hashcode, el, options, {signal: controller.controller.signal});
}

// noinspection JSUnusedGlobalSymbols
/** @param {string} event
 * @param {number} hashcode
 * @param {HTMLElement} el
 * @param {({start?: boolean; end?: boolean; cancel?: boolean; run?: boolean})} options
 * @returns void */
export function addTransitionEventsOnce(event, hashcode, el, options) {
    addEvents(event, hashcode, el, options, {once: true});
}

/** @param {string} event
 * @param {number} hashcode
 * @param {HTMLElement} el
 * @param {({start?: boolean; end?: boolean; cancel?: boolean; run?: boolean})} options
 * @param {AddEventListenerOptions} listenerOptions
 * @returns void */
function addEvents(event, hashcode, el, options, listenerOptions) {
    if (options.start) {
        el.addEventListener("transitionstart", args => dotNetTransitionEventAsync(args, event, hashcode, true),
            listenerOptions);
    }
    if (options.end) {
        el.addEventListener("transitionend", args => dotNetTransitionEventAsync(args, event, hashcode, true),
            listenerOptions);
    }
    if (options.cancel) {
        el.addEventListener("transitioncancel", args => dotNetTransitionEventAsync(args, event, hashcode, true),
            listenerOptions);
    }
    if (options.run) {
        el.addEventListener("transitionrun", args => dotNetTransitionEventAsync(args, event, hashcode, true),
            listenerOptions);
    }
}

// noinspection JSUnusedGlobalSymbols
/** @param {string} event
 * @param {number} hashcode
 * @returns void */
export function removeEvent(event, hashcode) {
    controllers = DOM.tryRemoveEvent(controllers, hashcode);
}

/** @param {TransitionEvent} args
 * @param {string} event
 * @param {number} hashcode
 * @param {boolean} once
 * @returns Promise<void> */
async function dotNetTransitionEventAsync(args, event, hashcode, once = false) {
    await DOM.invokeDotNetAsync(event, DOM.jsToCsharpAnimationEvent(args), hashcode, once);
}
