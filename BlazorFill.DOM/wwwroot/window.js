import {DOM} from "./dom.js";

/** @type ({controller: AbortController; hashcode: number})[] */
let controllers = [];

// noinspection JSUnusedGlobalSymbols
/** @param {string} event
 * @param {number} hashcode
 * @returns void */
export function addOnWindowClick(event, hashcode) {
    const controller = DOM.tryAddController(controllers, hashcode);
    if (controller)
        window.addEventListener("click", args => dotNetClickEventAsync(args, event, hashcode),
            {signal: controller.controller.signal});
}

// noinspection JSUnusedGlobalSymbols
/** @param {string} event
 * @param {number} hashcode
 * @returns void */
export function addOnWindowClickOnce(event, hashcode) {
    window.addEventListener("click", args => dotNetClickEventAsync(args, event, hashcode, true),
        {once: true});
}

// noinspection JSUnusedGlobalSymbols
/** @param {string} event
 * @param {number} hashcode
 * @returns void */
export function removeEvent(event, hashcode) {
    controllers = DOM.tryRemoveEvent(controllers, hashcode);
}

/** @param {MouseEvent} args
 * @param {string} event
 * @param {number} hashcode
 * @param {boolean} once
 * @returns Promise<void> */
async function dotNetClickEventAsync(args, event, hashcode, once = false) {
    await DOM.invokeDotNetAsync(event, DOM.jsToCsharpMouseEvent(args), hashcode, once);
}
