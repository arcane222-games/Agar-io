// External modules
const WebSocket = require('ws');

// Private variables

/**
 * @class ClientCache
 * @author Lee Hong Jun (ARC4NE22)
 * @description ClientCache for caching websocket client
 *              Last Modified: 2022. 05. 23
 */
class ClientCache {
    constructor() {
        this.size = 0;
        this.cache = {};
    }

    /**
     * @property
     * Get size of cache
     */
    getSize() {
        return this.size;
    }

    /**
     * @returns {WebSocket.WebSocket}
     */
    getClient(id) {
        return this.cache[id];
    }

    /**
     * 
     * @returns {object}
     */
    getCache() {
        return cache;
    }

    /**
     * Add client to cache
     * @param {WebSocket.WebSocket} ws
     */
    add(ws) {
        this.cache[this.size++] = ws;
    }

    /**
     * Remove client using id key
     * @param {number} id
     */
    removeByKey(id) {
        if (!Number.isInteger(id)) return;
        if (id < 0) return;

        if (cache[id] != undefined) {
            delete this.cache[id];
        }
    }

    /**
     * Remove client using ws value
     * @param {WebSocket.WebSocket} ws
     */
    removeByValue(ws) {
        const keys = Object.keys(this.cache);
        const wsKey = keys.find((key) => this.cache[key] === ws);

        if (keys.length > 0 && this.cache[wsKey] != undefined) {
            delete this.cache[wsKey];
        }
    }
}

module.exports = ClientCache;
