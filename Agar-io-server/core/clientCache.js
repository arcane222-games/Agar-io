// External modules
const WebSocket = require('ws');

// Private variables
let size = 0;
let cache = Object.freeze({});

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
    size() {
        return this.size;
    }

    /**
     * @returns {WebSocket.WebSocket}
     */
    getClient(id) {
        return this.cache[id];
    }

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
    removeById(id) {
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
    removeByWs(ws) {
        const key = Object.keys(this.cache).find((key) => object[key] === ws);
        if (id > -1 && this.cache[key] != undefined) {
            delete this.cache[key];
        }
    }
}

module.exports = ClientCache;
