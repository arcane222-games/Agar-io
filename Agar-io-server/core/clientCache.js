// External modules
const ws = require('ws');

// Private variables
let _size = 0;
let _cache = {};

/**
 * @class ClientCache
 * @author Lee Hong Jun (ARC4NE22)
 * @description ClientCache for caching websocket client
 *              Last Modified: 2022. 05. 23
 */
class ClientCache {
    constructor() {
    }

    /**
     * Get size of cache
     * @returns {number}
     */
    getSize() {
        return _size;
    }

    /**
     * Get client websocket by id
     * @param {number} id 
     * @returns {ws.WebSocket}
     */
    getClient(id) {
        return _cache[id];
    }

    /**
     * Get keys of cache
     * @returns {object}
     */
    getKeys() {
        return Object.keys(_cache);
    }

    /**
     * Add client to cache
     * @param {ws.WebSocket} ws
     */
    add(ws) {
        _cache[_size++] = ws;
    }

    /**
     * Remove client using id key
     * @param {number} id
     */
    removeByKey(id) {
        if (!Number.isInteger(id)) return;
        if (id < 0) return;

        if (_cache[id] != undefined) {
            delete _cache[id];
        }
    }

    /**
     * Remove client using ws value
     * @param {ws.WebSocket} ws
     */
    removeByValue(ws) {
        const keys = Object.keys(_cache);
        const wsKey = keys.find((key) => _cache[key] === ws);

        if (keys.length > 0 && _cache[wsKey] != undefined) {
            delete _cache[wsKey];
        }
    }
}

module.exports = ClientCache;
