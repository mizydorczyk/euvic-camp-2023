import {RadioChannel} from "./radioChannel";

export interface Piece {
  id: number
  title: string
  duration: string
  version: string
  artist: string
  releaseDate: string
  views: number
  programmeItemHeaders?: ProgrammeItemHeader[]
}

export interface ProgrammeItemHeader {
  playbackDate: string
  broadcastingDuration: string
  radioChannel: RadioChannel
  views: number
}
