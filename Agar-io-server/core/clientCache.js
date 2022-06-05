// External modules
const ws = require('ws');

// Private variables
let _idx = 0;
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
        return Object.keys(_cache).length;
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
     * Get id of clients (key values)
     * @returns {object}
     */
    getIds() {
        return Object.keys(_cache);
    }

    /**
     * Add client to cache
     * (add 'id' property to websocket object)
     * @param {ws.WebSocket} ws
     */
    add(ws) {
        ws.id = _idx;
        _cache[_idx++] = ws;
    }

    /**
     * Remove client using id key
     * @param {number} id
     */
    removeById(id) {
        if (!Number.isInteger(id)) return -1;
        if (id < 0) return -1;
        if (_cache[id] == undefined) return -1;

        delete _cache[id];
        return id;
    }

    /**
     * Remove client using ws value
     * @param {ws.WebSocket} ws
     * @returns {number} Id (key value) of websocket
     */
    removeByWS(ws) {
        const keys = Object.keys(_cache);
        const id = keys.find((key) => _cache[key] === ws);

        if (keys.length > 0 && _cache[id] != undefined) {
            delete _cache[id];
            return id;
        } else {
            return -1;
        }
    }
}

module.exports = ClientCache;
