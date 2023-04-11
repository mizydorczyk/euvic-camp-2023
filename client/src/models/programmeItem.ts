import {RadioChannel} from "./radioChannel";
import {Piece} from "./piece";

export interface ProgrammeItem {
  piece: Piece
  playbackDate: string
  broadcastingDuration: string
  radioChannel: RadioChannel
  views: number
}
