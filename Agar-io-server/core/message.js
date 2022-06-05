// External modules
const WebSocket = require('ws');

// Private variables
let _owner = null;
let _payload = null;

/**
 * @class Message
 * @author Lee Hong Jun (ARC4NE22)
 * @description Message for saving payload of websocket packet
 *              Last Modified: 2022. 06. 05
 */
class Message {
    /**
     * Constructor of Message class
     * @param {WebSocket.WebSocket} owner 
     * @param {object} payload 
     */
    constructor(owner, payload) {
        _owner = owner;
        _payload = payload;
    }

    /**
     * Get size of cache
     * @returns {WebSocket.WebSocket}
     */
    getOwner() {
        return _owner;
    }

    /**
     * @returns {WebSocket.WebSocket}
     */
    getPayload(id) {
        return _payload;
    }
}

module.exports = Message;
