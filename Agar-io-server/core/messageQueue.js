// External modules
const WebSocket = require('ws');
const Message = require('./message');

// Private variables
let _queue = [];

/**
 * @class MessageQueue
 * @author Lee Hong Jun (ARC4NE22)
 * @description MessageQueue for processing network messages
 *              Last Modified: 2022. 06. 05
 */
class MessageQueue {
    constructor() {
    }

    /**
     * @property
     * Get size of MessageQueue
     */
    getSize() {
        return _queue.length;
    }

    /**
     * Add Message to MessageQueue
     * @param {Message} msg
     */
    add(msg) {
        _queue.push(msg);
    }

    /**
     * Remove message from MessageQueue
     * @returns {Message}
     */
    pop() {
        return _queue.shift();
    }
}

module.exports = MessageQueue;
